<template>
	<div class="navbar-container">
		<div class="logo-container">
			<NavbarItem :activeClass="''" to="/">
				<VectorImage :path="booksLogo" :width="50" :height="30" />
				<span class="logo-text">BookShelf</span>
			</NavbarItem>
		</div>
		<div class="navbar-central-container">
			<NavbarItem to="/">Home</NavbarItem>
			<NavbarItem to="/my-books">My Books</NavbarItem>
			<div class="search-container">
				<SearchField />
			</div>
		</div>
		<div class="profile-icon-container">
			<ProfileDropdown />
		</div>
	</div>
</template>

<script>
	import NavbarItem from "./NavbarItem.vue";
	import VectorImage from "../../Ui/VectorImage.vue";
	import SearchField from "../../Ui/SearchField.vue";
	import ProfileDropdown from "./ProfileDropdown.vue";
	import { booksLogo } from "../../../assets/strings/icon-paths";

	export default {
		components: {
			NavbarItem,
			VectorImage,
			SearchField,
			ProfileDropdown,
		},
		setup: () => {
			return {
				booksLogo,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavbarComponent";
</style>
