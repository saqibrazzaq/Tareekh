import Common from "../utility/Common";
import { CityRes } from "./City";
import { CountryRes } from "./Country";
import { PagedReq } from "./PagedReq";
import { StateNameRes } from "./StateName";

export interface StateRes {
  stateId?: string;
  slug?: string;
  countryId?: string;
  country?: CountryRes;

  cities?: CityRes[];
  stateNames?: StateNameRes[];
}

export class StateReqEdit {
  slug?: string = "";
  countryId?: string = "";

  constructor(countryId?: string) {
    this.countryId = countryId;
  }
}

export class StateReqSearch extends PagedReq {
  countryId?: string;
  constructor(
    {
      pageNumber = 1,
      pageSize = Common.DEFAULT_PAGE_SIZE,
      orderBy = "",
      searchText = "",
    }: PagedReq,
    {countryId = ""}
  ) {
    super({
      pageNumber: pageNumber,
      pageSize: pageSize,
      orderBy: orderBy,
      searchText: searchText,
    });
    this.countryId = countryId;
  }
}