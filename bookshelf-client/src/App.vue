<template>
	<div class="sass-test">
		Sass test
		<user-item></user-item>
	</div>
</template>

<script>
import UserItem from "./components/UserItem.vue";

export default {
	name: "App",
	components: { UserItem },
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
