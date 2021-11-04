<template>
	<div class="menu-container">
		<ul>
			<li @click="profileItemHandler">
				<i class="fa fa-user fa-sm" aria-hidden="true"></i>
				<span>Profile</span>
			</li>
			<li @click="settingsItemHandler">
				<i class="fa fa-cog fa-sm" aria-hidden="true"></i>
				<span>Settings</span>
			</li>
			<li @click="logoutItemHandler">
				<i class="fa fa-sign-out-alt fa-sm" aria-hidden="true"></i>
				<span>Log Out</span>
			</li>
		</ul>
	</div>
</template>

<script>
	import { useStore } from "vuex";

	export default {
		setup: () => {
			const store = useStore();

			const profileItemHandler = () => {
				console.log("Profile");
			};
			const settingsItemHandler = () => {
				console.log("Settings");
			};
			const logoutItemHandler = () => {
				store.dispatch("auth/logout");
			};

			return {
				profileItemHandler,
				settingsItemHandler,
				logoutItemHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavbarMenu";
</style>
