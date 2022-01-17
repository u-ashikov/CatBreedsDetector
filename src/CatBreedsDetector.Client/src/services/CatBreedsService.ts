import axios from "axios";
import { getApiUrl } from "../helpers/apiHelpers";

// TODO: Should return a model containing information about the prediction or error.
export function uploadImage(imageToUpload: File) {
  const apiUrl = getApiUrl();

  const formData = new FormData();
  formData.append("catImage", imageToUpload);

  return axios.post(`${apiUrl}/api/CatBreeds/DetectAsync`, formData);
}
