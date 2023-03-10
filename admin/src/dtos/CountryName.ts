import Common from "../utility/Common";
import { CountryRes } from "./Country";
import { LanguageRes } from "./Language";
import { PagedReq } from "./PagedReq";

export interface CountryNameRes {
  countryNameId?: string;
  name?: string;

  languageId?: string;
  language?: LanguageRes;
  countryId?: string;
  country?: CountryRes;
}

export class CountryNameReqEdit {
  languageId?: string = "";
  countryId?: string = "";
  name?: string = "";

  constructor(languageId?: string, countryId?: string) {
    this.languageId = languageId;
    this.countryId = countryId;
  }
}

export class CountryNameReqSearch extends PagedReq {
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