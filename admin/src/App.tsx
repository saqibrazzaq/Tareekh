import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./layout/Layout";
import Countries from "./pages/country/Countries";
import CountryDelete from "./pages/country/CountryDelete";
import CountryEdit from "./pages/country/CountryEdit";
import CountryNames from "./pages/country/languages/CountryNames";
import CountryNamesDelete from "./pages/country/languages/CountryNamesDelete";
import CountryNamesEdit from "./pages/country/languages/CountryNamesEdit";
import Home from "./pages/home/Home";

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Layout />}>
          <Route index element={<Home />} />
          {/* Country */}
          <Route path="countries">
            <Route index element={<Countries />} />
            <Route path="edit" element={<CountryEdit />} />
            <Route path=":countryId">
              <Route path="edit" element={<CountryEdit />} />
              <Route path="delete" element={<CountryDelete />} />
              <Route path="languages">
                <Route index element={<CountryNames />} />
                <Route path="edit" element={<CountryNamesEdit />} />
                <Route path=":countryNameId/edit" element={<CountryNamesEdit />} />
                <Route path=":countryNameId/delete" element={<CountryNamesDelete />} />
              </Route>
            </Route>
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
