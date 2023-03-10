import Common from "../utility/Common";
import { CityRes } from "./City";
import { LanguageRes } from "./Language";
import { PagedReq } from "./PagedReq";

export interface CityNameRes {
  cityNameId?: string;
  name?: string;
  
  languageId?: string;
  language?: LanguageRes;
  
  cityId?: string;
  city?: CityRes;
}

export class CityNameReqEdit {
  languageId?: string = "";
  cityId?: string = "";
  name?: string = "";

  constructor(languageId?: string, cityId?: string) {
    this.languageId = languageId;
    this.cityId = cityId;
  }
}

export class CityNameReqSearch extends PagedReq {
  cityId?: string;
  constructor(
    {
      pageNumber = 1,
      pageSize = Common.DEFAULT_PAGE_SIZE,
      orderBy = "",
      searchText = "",
    }: PagedReq,
    {cityId = ""}
  ) {
    super({
      pageNumber: pageNumber,
      pageSize: pageSize,
      orderBy: orderBy,
      searchText: searchText,
    });
    this.cityId = cityId;
  }
}