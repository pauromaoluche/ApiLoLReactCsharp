import React from "react";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import Index from "./pages/index";

export default function Routers({ setUrlSplash }) {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" exact element={<Index setUrlSplash={setUrlSplash} />} />
      </Routes>
    </BrowserRouter>
  );
}
