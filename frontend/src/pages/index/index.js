import React from "react";
import {
  Container,
  Image,
  Tab,
  Tabs,
  Row,
  Col,
  ListGroup,
} from "react-bootstrap";

export default function Index() {
  return (
    <Container className="content">
      <div className="profile d-flex">
        <div className="image">
          <Image src="https://picsum.photos/150" thumbnail />
        </div>
        <div className="descriptions ms-4">
          <h2>Nick#tag</h2>
          <h3>Melhores tantos %</h3>
          <h4>Update</h4>
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
