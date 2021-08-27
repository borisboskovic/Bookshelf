<template>
	<div class="navbar-container">
		<div class="logo-container">
			<VectorImage :path="booksLogo" :width="50" :height="30" />
			<span class="logo-text">BookShelf</span>
		</div>
		<div class="navbar-central-container">
			<NavbarItem to="/">Početna</NavbarItem>
			<NavbarItem to="/auth">Auth</NavbarItem>
		</div>
		<div class="profile-icon-container">
			<ProfileDropdown />
		</div>
	</div>
</template>

<script>
	import NavbarItem from "./NavbarItem.vue";
	import VectorImage from "../../Ui/VectorImage.vue";
	import ProfileDropdown from "./ProfileDropdown.vue";
	import { booksLogo } from "../../../assets/strings/icon-paths";

	export default {
		components: {
			NavbarItem,
			VectorImage,
			ProfileDropdown,
		},
		setup() {
			return {
				booksLogo,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavbarComponent";
</style>
