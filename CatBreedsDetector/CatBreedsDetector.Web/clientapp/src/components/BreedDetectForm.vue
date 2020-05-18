<template>
    <div>
        <form method="post" enctype="multipart/form-data" novalidate v-if="isInitial || isSaving">
            <div class="dropbox">
                <label for="file" class="h6">Upload Cat Image</label>
                <input type="file" accept="image/*" v-bind:name="catImage" v-bind:disabled="isSaving" v-on:change="handleChange($event.target.name, $event.target.files)" class="input-file" />
                <p v-if="isInitial">
                    Drag your file here to begin<br> or click to browse
                </p>
                <p v-if="isSaving">
                    Uploading the file...
                    <img v-bind:src="this.url" class="img-responsive img-thumbnail" v-bind:alt="uploadedImage.originalName">
                </p>
            </div>
        </form>
        <div v-if="isSuccess">
            <h2>File uploaded successfully.</h2>
            <p>
                <a href="javascript:void(0)" @click="reset()">Upload again</a>
            </p>
            <h1>Your cats is: {{ predictedBreed }}</h1>
            <img v-bind:src="this.url" class="img-responsive img-thumbnail">
        </div>
        <!--FAILED-->
        <div v-if="isFailed">
            <h2>Uploaded failed.</h2>
            <p>
                <a href="javascript:void(0)" @click="reset()">Try again</a>
            </p>
            <pre>{{ uploadError }}</pre>
        </div>
    </div>
</template>

<script>
    import fileService from '../service/file-service.js';

    const STATUS_INITIAL = 0, STATUS_SAVING = 1, STATUS_SUCCESS = 2, STATUS_FAILED = 3;

    export default {
        data: function () {
            return {
                uploadedImage: null,
                uploadError: null,
                currentStatus: null,
                catImage: 'cat-image',
                url: null,
                predictedBreed: null
            }
        },
        computed: {
            isInitial: function () {
                return this.currentStatus === STATUS_INITIAL;
            },
            isSaving: function () {
                return this.currentStatus === STATUS_SAVING;
            },
            isSuccess: function () {
                return this.currentStatus === STATUS_SUCCESS;
            },
            isFailed: function () {
                return this.currentStatus === STATUS_FAILED;
            }
        },
        methods: {
            upload: function (formData) {
                this.currentStatus = STATUS_SAVING;

                fileService.upload(formData)
                    .then(x => {
                        // eslint-disable-next-line no-console
                        console.log(x);
                        this.currentStatus = STATUS_SUCCESS;
                        this.predictedBreed = x.data;
                    })
                    .catch(err => {
                        this.uploadError = err.response;
                        this.currentStatus = STATUS_FAILED;
                    });
            },
            reset: function () {
                this.uploadedImage = null;
                this.currentStatus = STATUS_INITIAL;
                this.uploadError = null;
                this.url = null;
            },
            handleChange: function (fieldName, files) {
                const formData = new FormData();

                if (!files.length) {
                    return;
                }

                this.uploadedImage = files[0];

                formData.append('catImage', files[0]);

                this.url = URL.createObjectURL(files[0]);

                this.upload(formData);
            }
        },
        mounted: function () {
            this.reset();
        }
    }
</script>

<style>
    .dropbox {
        outline: 2px dashed grey; /* the dash box */
        outline-offset: -10px;
        background: lightcyan;
        color: dimgray;
        padding: 10px 10px;
        min-height: 200px; /* minimum height */
        position: relative;
        cursor: pointer;
    }

    .input-file {
        opacity: 0; /* invisible but it's there! */
        width: 100%;
        height: 200px;
        position: absolute;
        cursor: pointer;
    }

    .dropbox:hover {
        background: lightblue; /* when mouse over to the drop zone, change color */
    }

    .dropbox p {
        font-size: 1.2em;
        text-align: center;
        padding: 50px 0;
    }
</style>