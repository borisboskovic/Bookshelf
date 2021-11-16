<template>
	<div class="navbar-dropdown-container" @click="toggleDropdownOpened">
		<i class="fa fa-caret-down fa-lg" aria-hidden="true"></i>
		<div class="avatar-photo-container">
			<FallbackImage :source="avatar" :defaultImage="defaultImage" title="Avatar photo" />
		</div>
		<NavbarMenu v-if="isDropdownOpened" @dismissmenu="toggleDropdownOpened" />
	</div>
</template>

<script>
	import { ref, computed } from "vue";
	import { useStore } from "vuex";
	import NavbarMenu from "./NavbarMenu.vue";
	import FallbackImage from "@/components/Ui/FallbackImage.vue";
	import defaultImage from "@/assets/images/rasters/avatar-placeholder.png";

	export default {
		components: {
			NavbarMenu,
			FallbackImage,
		},
		setup: () => {
			const { state } = useStore();
			const isDropdownOpened = ref(false);

			const toggleDropdownOpened = () => {
				isDropdownOpened.value = !isDropdownOpened.value;
			};

			const avatar = computed(() => state.auth.avatar);

			return {
				avatar,
				isDropdownOpened,
				toggleDropdownOpened,
				defaultImage,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavbarDropdown";
</style>
