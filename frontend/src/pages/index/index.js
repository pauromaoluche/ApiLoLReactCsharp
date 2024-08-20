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

  useEffect(() => {}, []);

  async function loadSumonner(event) {
    event.preventDefault();
    const fullNick = event.target.elements.nickName.value;
    const [nick, tag] = fullNick.split("#");
    try {
      const response = await api.get(`api/summoner/${nick}/${tag}`);
      setNickName(response.data.account.gameName);
      setTagLine(response.data.account.tagLine);
      setUrlSplash(response.data.topMastery[0].urlSplash);
      setLvl(response.data.summoner.summonerLevel);
      setIcon(response.data.summoner.urlIcon);
      console.log(response.data.topMastery[0].urlSplash);
    } catch (error) {
      console.error("Erro ao carregar summoner:", error);
    }
  }

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
          <Form onSubmit={loadSumonner}>
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
                <Col md="4">
                  <div className="left">
                    {" "}
                    <ListGroup as="ol" numbered>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                    </ListGroup>
                  </div>
                </Col>
                <Col md="8">
                  {" "}
                  <div className="right">
                    {" "}
                    <ListGroup as="ol" numbered>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
                      <ListGroup.Item as="li">Cras justo odio</ListGroup.Item>
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
      <div className="resultActions"></div>
    </Container>
  );
}
