import { LanguageReqEdit, LanguageReqSearch } from "../dtos/Language";
import { api } from "./axiosconfig";

export const LanguageApi = {
  search: async function (searchParams: LanguageReqSearch) {
    const response = await api.request({
      url: "/languages/search",
      method: "GET",
      params: searchParams,
    })

    return response.data
  },
  get: async function (languageId?: string) {
    if (!languageId) return {};
    const response = await api.request({
      url: `/languages/` + languageId,
      method: "GET",
    })

    return response.data
  },
  count: async function () {
    const response = await api.request({
      url: `/languages/count`,
      method: "GET",
    })

    return response.data
  },
  create: async function (language: LanguageReqEdit) {
    const response = await api.request({
      url: `/languages`,
      method: "POST",
      data: language,
    })

    return response.data
  },
  update: async function (languageId?: string, language?: LanguageReqEdit) {
    await api.request({
      url: `/languages/` + languageId,
      method: "PUT",
      data: language,
    })
  },
  delete: async function (languageId?: string) {
    const response = await api.request({
      url: `/languages/` + languageId,
      method: "DELETE",
    })

    return response.data
  },
}