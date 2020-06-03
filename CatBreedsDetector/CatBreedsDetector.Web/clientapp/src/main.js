import Vue from 'vue'
import App from './App.vue'
import VueRouter from 'vue-router'
import { router } from './routing/router'

import 'jquery/src/jquery.js'
import 'popper.js/dist/popper.min.js'
import 'bootstrap/dist/css/bootstrap.min.css'
import '@fortawesome/fontawesome-free/css/all.min.css'

Vue.config.productionTip = false;
Vue.use(VueRouter);

new Vue({
    el: '#app',
    router,
    render: h => h(App)
});
