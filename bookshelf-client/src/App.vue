<template>
  <div class="sass-test">Sass test</div>
  Hello World
</template>

<script>
export default {
  name: "App",
  components: {},
};
</script>

<style lang="scss">
.sass-test {
  &:hover {
    color: red;
    cursor: pointer;
  }
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
