<template>
	<img class="avatar-photo" :src="defaultImage" alt="Avatar image" />
</template>

<script>
	import defaultImage from "../../../assets/images/rasters/avatar-placeholder.png";
	export default {
		setup: () => {
			return {
				defaultImage,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./ProfileDropdown";
</style>
