import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://localhost:59999/api",
});

// TODO: Should return a model containing information about the prediction or error.
export function upload(formData: FormData) {
  return axiosInstance.post("/CatBreeds/DetectAsync", formData);
}
