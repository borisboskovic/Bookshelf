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
			<NavigationSelect />
			<div class="search-container">
				<SearchField />
			</div>
		</div>
		<div class="profile-icon-container">
			<NavbarDropdown />
		</div>
	</div>
</template>

<script>
	import NavbarItem from "./NavbarItem.vue";
	import NavigationSelect from "./NavigationSelect.vue";
	import VectorImage from "@/components/Ui/Imaging/VectorImage.vue";
	import SearchField from "@/components/Ui/SearchField.vue";
	import NavbarDropdown from "./NavbarDropdown.vue";
	import { booksLogo } from "@/assets/strings/icon-paths";

	export default {
		components: {
			NavbarItem,
			NavigationSelect,
			VectorImage,
			SearchField,
			NavbarDropdown,
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
