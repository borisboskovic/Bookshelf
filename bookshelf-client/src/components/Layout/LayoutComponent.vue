<template>
	<div class="layout-container">
		<NavbarComponent />
		<main>
			<slot />
		</main>
	</div>
</template>

<script>
	import NavbarComponent from "./Navbar/NavbarComponent";

	export default {
		components: {
			NavbarComponent,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./LayoutComponent.scss";
</style>
