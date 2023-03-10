import Common from "../utility/Common";
import { CountryNameRes } from "./CountryName";
import { PagedReq } from "./PagedReq";
import { StateRes } from "./State";

export interface CountryRes {
  countryId?: string;
  slug?: string;
  states?: StateRes[];
  countryNames?: CountryNameRes[];
}

export class CountryReqEdit {
  slug?: string = "";
}

export class CountryReqSearch extends PagedReq {
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
