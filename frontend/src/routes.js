import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Index from "./pages/index";

export default function Routers() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" exact Component={Index} />
      </Routes>
    </BrowserRouter>
  );
}
