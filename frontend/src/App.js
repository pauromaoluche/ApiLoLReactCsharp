import React from "react";
import Header from "./components/header";
import Nav from "./components/navbar";
import Footer from "./components/footer";
import Banner from "./components/banner";
import "./styles.css";

const App = () => (
  <div>
    <Header />
    <Nav />
    <Banner />
    <Footer />
  </div>
);

export default App;
