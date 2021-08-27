<template>
	<LayoutComponent v-if="isLoggedIn">
		<router-view />
	</LayoutComponent>
	<router-view v-else/>
</template>

<script>
	import {computed} from "vue";
	import {useStore} from "vuex";
	import LayoutComponent from "./components/Layout/LayoutComponent.vue";

	export default {
		components: { LayoutComponent },
		name: "App",

		setup: () => {
			const store = useStore();

			const isLoggedIn = computed(() => store.state.auth.isLoggedIn);

			console.log("Is logged in", isLoggedIn.value);

			return {
				isLoggedIn
			}
		}
	};
</script>

<style lang="scss">
	@import "./assets/style/main.scss";
</style>
