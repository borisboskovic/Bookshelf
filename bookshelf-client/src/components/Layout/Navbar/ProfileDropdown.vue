<template>
	<div class="profile-dropdown-container" @click="toggleDropdownOpened">
		<i class="fa fa-caret-down fa-lg" aria-hidden="true"></i>
		<img class="avatar-photo" :src="defaultImage" alt="Avatar image" />
		<ProfileMenu v-if="isDropdownOpened" />
	</div>
</template>

<script>
	import { ref } from "vue";
	import ProfileMenu from "./ProfileMenu.vue";
	import defaultImage from "../../../assets/images/rasters/avatar-placeholder.png";
	export default {
		components: {
			ProfileMenu,
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
	@import "./ProfileDropdown";
</style>
