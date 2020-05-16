<template>
    <div>
        <h1>Hello, this is the home page</h1>
        <breed-detect-form></breed-detect-form>
    </div>
</template>

<script>
    import BreedDetectForm from './BreedDetectForm.vue'

    export default {
        components: {
            'breed-detect-form': BreedDetectForm
        }
    }
</script>