<template>
	<LayoutComponent v-if="isLoggedIn">
		<router-view />
	</LayoutComponent>
	<router-view v-else />
</template>

<script>
	import { computed, onMounted } from "vue";
	import { useStore } from "vuex";
	import LayoutComponent from "./components/Layout/LayoutComponent.vue";

	export default {
		components: { LayoutComponent },
		name: "App",

		setup: () => {
			const store = useStore();

			const isLoggedIn = computed(() => store.state.auth.isLoggedIn);

			onMounted(() => {
				store.dispatch("auth/persistedLogin");
			});

			return {
				isLoggedIn,
			};
		},
	};
</script>

<style lang="scss">
	@import "./assets/style/main.scss";
</style>
