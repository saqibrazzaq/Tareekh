import Common from "../utility/Common";
import { PagedReq } from "./PagedReq";

export interface TimezoneRes {
  timezoneId?: string;
  tzName?: string;
  cityName?: string;
  gmtOffset?: number;
  gmtOffsetName?: string;
  abbreviation?: string;
}

export class TimezoneReqEdit {
  tzName?: string = "";
  cityName?: string = "";
  gmtOffset?: number = 0;
  gmtOffsetName?: string = "";
  abbreviation?: string = "";
}

export class TimezoneReqSearch extends PagedReq {
  constructor(
    {
      pageNumber = 1,
      pageSize = Common.DEFAULT_PAGE_SIZE,
      orderBy,
      searchText = "",
    }: PagedReq,
    {}
  ) {
    super({
      pageNumber: pageNumber,
      pageSize: pageSize,
      orderBy: orderBy,
      searchText: searchText,
    });
  }
}