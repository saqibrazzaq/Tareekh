import { BrowserRouter, Route, Routes } from "react-router-dom";
import Layout from "./layout/Layout";
import Countries from "./pages/country/Countries";
import CountryDelete from "./pages/country/CountryDelete";
import CountryEdit from "./pages/country/CountryEdit";
import CountryNames from "./pages/country/names/CountryNames";
import CountryNamesDelete from "./pages/country/names/CountryNamesDelete";
import CountryNamesEdit from "./pages/country/names/CountryNamesEdit";
import StateNameDelete from "./pages/country/states/names/StateNameDelete";
import StateNameEdit from "./pages/country/states/names/StateNameEdit";
import StateNames from "./pages/country/states/names/StateNames";
import StateDelete from "./pages/country/states/StateDelete";
import StateEdit from "./pages/country/states/StateEdit";
import States from "./pages/country/states/States";
import Home from "./pages/home/Home";
import LanguageDelete from "./pages/language/LanguageDelete";
import LanguageEdit from "./pages/language/LanguageEdit";
import Languages from "./pages/language/Languages";
import TimezoneDelete from "./pages/timezone/TimezoneDelete";
import TimezoneEdit from "./pages/timezone/TimezoneEdit";
import Timezones from "./pages/timezone/Timezones";

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
              {/* Country Names */}
              <Route path="names">
                <Route index element={<CountryNames />} />
                <Route path="edit" element={<CountryNamesEdit />} />
                <Route path=":countryNameId/edit" element={<CountryNamesEdit />} />
                <Route path=":countryNameId/delete" element={<CountryNamesDelete />} />
              </Route>
              {/* States */}
              <Route path="states">
                <Route index element={<States />} />
                <Route path="edit" element={<StateEdit />} />
                <Route path=":stateId">
                  <Route path="edit" element={<StateEdit />} />
                  <Route path="delete" element={<StateDelete />} />
                  {/* State Names */}
                  <Route path="names">
                    <Route index element={<StateNames />} />
                    <Route path="edit" element={<StateNameEdit />} />
                    <Route path=":stateNameId">
                      <Route path="edit" element={<StateNameEdit />} />
                      <Route path="delete" element={<StateNameDelete />} />
                    </Route>
                  </Route>
                </Route>
              </Route>
            </Route>
          </Route>
          {/* Language */}
          <Route path="languages">
            <Route index element={<Languages />} />
            <Route path="edit" element={<LanguageEdit />} />
            <Route path=":languageId/edit" element={<LanguageEdit />} />
            <Route path=":languageId/delete" element={<LanguageDelete />} />
          </Route>
          {/* Timezone */}
          <Route path="timezones">
            <Route index element={<Timezones />} />
            <Route path="edit" element={<TimezoneEdit />} />
            <Route path=":timezoneId/edit" element={<TimezoneEdit />} />
            <Route path=":timezoneId/delete" element={<TimezoneDelete />} />
          </Route>
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
