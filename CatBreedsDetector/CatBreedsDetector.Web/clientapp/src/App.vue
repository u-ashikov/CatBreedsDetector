<template>
  <div id="app" class="container">
      <img class="img-thumbnail rounded-circle" src="../public/cat-paw-logo.png"/>
      <h1 class="display-3 mt-3">Cat Breeds Detector</h1>
      <hr />
      <NavBar></NavBar>
      <Footer></Footer>
  </div>
</template>

<script>
import NavBar from './components/NavBar.vue'
import Footer from './components/Footer.vue'

export default {
  name: 'app',
  components: {
      NavBar,
      Footer
  }
}
</script>

<style>
@font-face {
  font-family: 'Indie-Flower';
  src: url('../public/fonts/IndieFlower-Regular.ttf');
}

#app {
    font-family: Indie-Flower;
    font-size: 1.4em;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    text-align: center;
    color: #2c3e50;
}
</style>
