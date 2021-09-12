<template>
	<div class="navbar-dropdown-container" @click="toggleDropdownOpened">
		<i class="fa fa-caret-down fa-lg" aria-hidden="true"></i>
		<img class="avatar-photo" :src="defaultImage" alt="Avatar image" />
		<NavbarMenu v-if="isDropdownOpened" />
	</div>
</template>

<script>
	import { ref } from "vue";
	import NavbarMenu from "./NavbarMenu.vue";
	import defaultImage from "@/assets/images/rasters/avatar-placeholder.png";
	export default {
		components: {
			NavbarMenu,
		},
		setup: () => {
			const isDropdownOpened = ref(false);

			const toggleDropdownOpened = () => {
				isDropdownOpened.value = !isDropdownOpened.value;
			};

			return {
				defaultImage,
				isDropdownOpened,
				toggleDropdownOpened,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavbarDropdown";
</style>
