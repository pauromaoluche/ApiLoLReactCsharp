import React, { useState } from "react";
import Header from "./components/header";
import Nav from "./components/navbar";
import Footer from "./components/footer";
import Banner from "./components/banner";
import Routers from "./routes";

import "./App.scss";

const App = () => {
  const [urlSplash, setUrlSplash] = useState(null);

  return (
    <>
      <Header />
      <Nav />
      <Banner urlSplash={urlSplash} />
      <div className="content">
        <Routers setUrlSplash={setUrlSplash} />
      </div>
      <Footer />
    </>
  );
};

export default App;
