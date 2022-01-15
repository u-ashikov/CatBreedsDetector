import axios from "axios";
import { getApiUrl } from "../helpers/apiHelpers";

// TODO: Should return a model containing information about the prediction or error.
export function upload(formData: FormData) {
  const apiUrl = getApiUrl();

  return axios.post(`${apiUrl}/api/CatBreeds/DetectAsync`, formData);
}
