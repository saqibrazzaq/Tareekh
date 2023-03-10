import Common from "../utility/Common";
import { PagedReq } from "./PagedReq";

export interface LanguageRes {
  languageId?: string;
  languageCode?: string;
  name?: string;
}

export class LanguageReqEdit {
  languageCode?: string = "";
  name?: string = "";
}

export class LanguageReqSearch extends PagedReq {
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