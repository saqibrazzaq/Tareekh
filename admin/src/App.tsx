import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./layout/Layout";
import Home from "./pages/home/Home";

function App() {
  return (
    <BrowserRouter>
      <Route path="/" element={<Layout />}>
        <Route index element={<Home />} />
        
      </Route>
    </BrowserRouter>
  );
}

export default App;
