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
	import { logout } from "@/services/auth-service";
	export default {
		setup: () => {
			const profileItemHandler = () => {
				console.log("Profile");
			};
			const settingsItemHandler = () => {
				console.log("Settings");
			};
			const logoutItemHandler = () => {
				console.log("Logout");
				logout();
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
