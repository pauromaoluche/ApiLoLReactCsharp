import React from "react";
import Header from "./components/header";
import Nav from "./components/navbar";
import Footer from "./components/footer";
import Banner from "./components/banner";
import Routers from "./routes";

import "./App.scss";

const App = () => (
  <>
    <Header />
    <Nav />
    <Banner />
    <div className="content">
      <Routers />
    </div>
    <Footer />
  </>
);

export default App;
