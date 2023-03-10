import Common from "../utility/Common";
import { LanguageRes } from "./Language";
import { PagedReq } from "./PagedReq";
import { StateRes } from "./State";

export interface StateNameRes {
  stateNameId?: string;
  name?: string;

  languageId?: string;
  language?: LanguageRes;
  stateId?: string;
  state?: StateRes;
}

export class StateNameReqEdit {
  languageId?: string = "";
  stateId?: string = "";
  name?: string = "";

  constructor(languageId?: string, stateId?: string) {
    this.languageId = languageId;
    this.stateId = stateId;
  }
}

export class StateNameReqSearch extends PagedReq {
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