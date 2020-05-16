<template>
    <form method="post" enctype="multipart/form-data" novalidate v-if="isInitial || isSaving">
        <div class="dropbox">
            <label for="file" class="h6">Upload Cat Image</label>
            <input type="file" accept="img/*" v-bind:name="uploadedFieldName" v-bind:disabled="isSaving" class="input-file" />
            <p v-if="isInitial">
                Drag your file here to begin<br> or click to browse
            </p>
            <p v-if="isSaving">
                Uploading the file...
            </p>
        </div>

        <input type="submit" class="btn btn-success" value="Check Breed" v-on:submit="submitForm()"/>
    </form>
</template>

<script>
    const STATUS_INITIAL = 0, STATUS_SAVING = 1, STATUS_SUCCESS = 2, STATUS_FAILED = 3;

    export default {
        data: function () {
            return {
                uploadedImage: null,
                uploadError: null,
                currentStatus: null,
                uploadedFieldName: 'cat-image'
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
                //evt.preventDefault();
                // eslint-disable-next-line no-console
                console.log(formData);
            },
            reset: function () {
                this.uploadedImage = null;
                this.currentStatus = STATUS_INITIAL;
                this.uploadError = null;
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