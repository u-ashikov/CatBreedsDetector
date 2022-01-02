import VueRouter from 'vue-router'

import Home from '../components/Home'
import About from '../components/About'

const router = new VueRouter({
    routes: [
        { path: '', component: Home },
        { path: '/about', component: About }
    ],
    mode: "history"
});

export { router }