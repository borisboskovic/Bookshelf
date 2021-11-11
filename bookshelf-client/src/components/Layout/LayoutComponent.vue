<template>
	<div class="layout-container">
		<NavbarComponent />
		<main>
			<slot />
		</main>
	</div>
</template>

<script>
	import { onMounted } from "vue";
	import { useStore } from "vuex";
	import NavbarComponent from "./Navbar/NavbarComponent";

	export default {
		components: {
			NavbarComponent,
		},
		setup: () => {
			const { dispatch } = useStore();

			onMounted(() => {
				dispatch("auth/fetchUserInfo");
			});
		},
	};
</script>

<style lang="scss" scoped>
	@import "./LayoutComponent.scss";
</style>
