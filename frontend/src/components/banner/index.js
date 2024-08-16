import React from "react";

const Banner = ({ urlSplash }) => (
  <div
    className="banner"
    style={{
      backgroundImage: `url(${urlSplash})`,
      backgroundSize: "cover",
    }}
  >
    <h1>Bem-vindo ao League of Graphs</h1>
    <p>Encontre as melhores informações sobre campeões e builds.</p>
  </div>
);

export default Banner;
