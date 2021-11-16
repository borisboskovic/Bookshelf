<template>
	<teleport to="#modal-container">
		<div class="menu-backdrop" @click="dismissMenu">
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
		</div>
	</teleport>
</template>

<script>
	import { useStore } from "vuex";

	export default {
		emits: ["dismissmenu"],
		setup: (_, context) => {
			const store = useStore();

			const dismissMenu = () => {
				context.emit("dismissmenu");
			};

			const profileItemHandler = () => {};
			const settingsItemHandler = () => {};
			const logoutItemHandler = () => {
				store.dispatch("auth/logout");
			};

			return {
				profileItemHandler,
				settingsItemHandler,
				logoutItemHandler,
				dismissMenu,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavbarMenu";
</style>
