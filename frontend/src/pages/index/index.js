import React, { useState, useEffect } from "react";
import api from "../../services/api";
import "./styles.scss";
import {
  Container,
  Image,
  Tab,
  Tabs,
  Row,
  Col,
  ListGroup,
  Form,
} from "react-bootstrap";

export default function Index({ setUrlSplash }) {
  const [nickName, setNickName] = useState("");
  const [tagLine, setTagLine] = useState("");
  const [lvl, setLvl] = useState(0);
  const [icon, setIcon] = useState("");
  const [matches, setMaches] = useState([]);
  const [puuid, setPuuid] = useState(null);

  useEffect(() => {
    if (puuid) {
      getMatches(puuid);
    }
  }, [puuid]);

  async function loadSummoner(event) {
    event.preventDefault();
    const fullNick = event.target.elements.nickName.value;
    const [nick, tag] = fullNick.split("#");
    try {
      const response = await api.get(`api/summoner/info/${nick}/${tag}`);

      setTagLine(response.data.account.tagLine);
      setNickName(response.data.account.gameName);
      setUrlSplash(response.data.topMastery[0].urlSplash);
      setLvl(response.data.summoner.summonerLevel);
      setIcon(response.data.summoner.urlIcon);
      setPuuid(response.data.account.puuid);
    } catch (error) {
      console.error("Erro ao carregar summoner:", error);
    }
  }

  async function getMatches(puuid) {
    try {
      const response = await api.get(`api/summoner/match/${puuid}`);
      setMaches(response.data);
    } catch (error) {}
  }

  const renderParticipant = (participant, index) => (
    <ListGroup.Item key={index}>
      <small>
        {participant.riotIdGameName}#{participant.riotIdTagline}
      </small>
    </ListGroup.Item>
  );

  return (
    <Container className="content">
      <div className="profile-header d-flex justify-content-between">
        <div className="profile d-flex">
          <div className="image">
            <Image width="150px" src={icon} thumbnail />
            <div className="level">{lvl}</div>
          </div>
          <div className="descriptions ms-4">
            <h2>
              {nickName}#{tagLine}
            </h2>
            <h3>Melhores tantos %</h3>
            <h4>Update</h4>
          </div>
        </div>
        <div className="searchBar">
          <Form onSubmit={loadSummoner}>
            <Form.Control
              type="text"
              placeholder="Search"
              className=" mr-sm-2"
              name="nickName"
            />
          </Form>
        </div>
      </div>
      <div className="actions">
        <Tabs
          defaultActiveKey="home"
          id="uncontrolled-tab-example"
          className="mb-3"
        >
          <Tab eventKey="home" title="Visão geral">
            <div className="contentOverview">
              <Row>
                <Col md="3">
                  <div className="left">
                    <ListGroup as="ol" numbered>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                    </ListGroup>
                  </div>
                </Col>
                <Col md="9">
                  <div className="right">
                    <ListGroup as="ol">
                      {matches.map((match) => {
                        const participant = match.info.participants.find(
                          (p) => p.puuid === puuid
                        );
                        const teamLeft = match.info.participants.filter(
                          (p) => p.teamId === 100
                        );
                        const teamRight = match.info.participants.filter(
                          (p) => p.teamId === 200
                        );
                        return (
                          <ListGroup.Item key={match.metadata.matchId}>
                            <div className="content-match">
                              <Row>
                                <Col xl="2">
                                  <div className="group-one">
                                    <div className="row-one">
                                      <div className="queue-type">
                                        <small>Ranqueda</small>
                                      </div>
                                      <div className="departureDate">
                                        <small>2 meses</small>
                                      </div>
                                    </div>
                                    <div className="row-two">
                                      <span>+25pdl</span>
                                    </div>
                                    <div className="row-three">
                                      <span className="result">Win</span>
                                      <span className="time">35:40</span>
                                    </div>
                                  </div>
                                </Col>
                                <Col xl="3">
                                  <div className="group-two">
                                    <div className="items">
                                      <div className="champion">
                                        <Image src={participant.urlChampIcon} />
                                      </div>
                                      <div className="summoner-spells">
                                        <Image src="https://picsum.photos/30" />
                                        <Image
                                          src="https://picsum.photos/30"
                                          className="fluid"
                                        />
                                      </div>
                                      <div className="runes">
                                        <Image src="https://picsum.photos/30" />
                                        <Image src="https://picsum.photos/30" />
                                      </div>
                                    </div>
                                  </div>
                                </Col>
                                <Col xl="2">
                                  <div className="group-three">
                                    <div className="row-one">
                                      <div className="kda">25/3/10</div>
                                      <div className="kda-info">0.40 KDA</div>
                                    </div>
                                    <div className="row-two">
                                      <div className="cs">250cs</div>
                                      <div className="vision">30 vision</div>
                                    </div>
                                  </div>
                                </Col>
                                <Col xl="2">
                                  <div className="group-four">
                                    <div className="items">
                                      <div className="main">
                                        <Image src="https://picsum.photos/20" />
                                        <Image src="https://picsum.photos/20" />
                                        <Image src="https://picsum.photos/20" />
                                        <Image src="https://picsum.photos/20" />
                                        <Image src="https://picsum.photos/20" />
                                        <Image src="https://picsum.photos/20" />
                                      </div>
                                      <div className="trincket">
                                        <Image src="https://picsum.photos/30" />
                                      </div>
                                    </div>
                                  </div>
                                </Col>
                                <Col xl="3">
                                  <div className="group-five d-flex">
                                    <div className="left">
                                      <ListGroup>
                                        {teamLeft.map((participant) =>
                                          renderParticipant(
                                            participant,
                                            participant.participantId
                                          )
                                        )}
                                      </ListGroup>
                                    </div>
                                    <div className="right">
                                      <ListGroup>
                                        {teamRight.map((participant) =>
                                          renderParticipant(
                                            participant,
                                            participant.participantId
                                          )
                                        )}
                                      </ListGroup>
                                    </div>
                                  </div>
                                </Col>
                              </Row>
                            </div>
                          </ListGroup.Item>
                        );
                      })}
                    </ListGroup>
                  </div>
                </Col>
              </Row>
            </div>
          </Tab>
          <Tab eventKey="championStatus" title="Status campeões">
            <ListGroup as="ol" numbered>
              <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
              <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
              <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
            </ListGroup>
          </Tab>
          <Tab eventKey="liveGame" title="Partida ao vivo">
            Partida ao vivo
          </Tab>
        </Tabs>
      </div>
      <div className="resultActions">
      </div>
    </Container>
  );
}
