<template>
    <div class="container">
        <div class="row">
            <div class="col-3">
                <div class="nav flex-column nav-pills" id="v-pills-tab" role="tablist" aria-orientation="vertical">
                    <router-link tag="a" class="nav-link" id="v-pills-home-tab" to="/" v-bind:class="{ active: active === 'home' }" v-on:click="active = home">Home</router-link>
                    <router-link tag="a" class="nav-link" id="v-pills-about-tab" to="/about">About</router-link>
                </div>
            </div>
            <div class="col-9">
                <div class="tab-content" id="v-pills-tabContent">
                    <router-view></router-view>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    export default {
        data: function () {
            return {
                active: null
            }
        }
    }
</script>