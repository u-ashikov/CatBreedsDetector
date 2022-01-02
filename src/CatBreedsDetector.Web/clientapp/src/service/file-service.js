import axios from 'axios';

var axiosInstance = axios.create({
    baseURL: 'http://localhost:59999/api'
});

function upload(formData) {
    return axiosInstance.post('/CatBreeds/DetectAsync', formData);
}

export default {
    upload: upload
};