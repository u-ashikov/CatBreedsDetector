import axios from "axios";
import { getApiUrl } from "../helpers/apiHelpers";
import { ICatBreedDetectionResult } from "../models/detection/ICatBreedDetectionResult";

export function uploadImage(imageToUpload: File) {
  const apiUrl = getApiUrl();

  const formData = new FormData();
  formData.append("catImage", imageToUpload);

  return axios.post<ICatBreedDetectionResult>(
    `${apiUrl}/api/CatBreeds/DetectAsync`,
    formData
  );
}
