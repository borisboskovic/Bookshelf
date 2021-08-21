<template>
	<div class="main-container">
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

	.main-container {
		display: flex;
		flex-direction: column;
		background-color: bisque;
		height: 100%;
	}

	main {
		flex-grow: 1;
	}
</style>
