import Common from "../utility/Common";
import { CityNameRes } from "./CityName";
import { PagedReq } from "./PagedReq";
import { StateRes } from "./State";
import { TimezoneRes } from "./Timezone";

export interface CityRes {
  cityId?: string;
  slug?: string;
  latitude?: number;
  longitude?: number;

  stateId?: string;
  state?: StateRes;

  timezoneId?: string;
  timezone?: TimezoneRes;

  cityNames?: CityNameRes[];
}

export class CityReqEdit {
  slug?: string = "";
  latitude?: number = 0;
  longitude?: number = 0;
  stateId?: string = "";
  timezoneId?: string = "";

  constructor(stateId?: string) {
    this.stateId = stateId;
  }
}

export class CityReqSearch extends PagedReq {
  stateId?: string;
  constructor(
    {
      pageNumber = 1,
      pageSize = Common.DEFAULT_PAGE_SIZE,
      orderBy = "",
      searchText = "",
    }: PagedReq,
    {stateId = ""}
  ) {
    super({
      pageNumber: pageNumber,
      pageSize: pageSize,
      orderBy: orderBy,
      searchText: searchText,
    });
    this.stateId = stateId;
  }
}