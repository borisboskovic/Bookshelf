<template>
	<LayoutComponent>
		<router-view />
	</LayoutComponent>
</template>

<script>
	import LayoutComponent from "./components/Layout/LayoutComponent.vue";

	export default {
		components: { LayoutComponent },
		name: "App",
	};
</script>

<style lang="scss">
	@import "./assets/style/main.scss";
</style>
