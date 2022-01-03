import axios from "axios";

const axiosInstance = axios.create({
  baseURL: "http://localhost:59999/api",
});

export function upload(formData: FormData) {
  return axiosInstance.post("/CatBreeds/DetectAsync", formData);
}
