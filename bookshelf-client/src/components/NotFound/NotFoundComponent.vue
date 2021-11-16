<template>
	<div class="not-found-component">
		<div class="top-container">
			<img :src="image" alt="Book with torn page" />
		</div>
		<div class="bottom-container">
			<span class="text-large">404</span>
			<span class="text-large">|</span>
			<span>Page Not Found</span>
		</div>
	</div>
</template>

<script>
	import image from "@/assets/images/rasters/404-torn-page.png";

	export default {
		setup: () => {
			return {
				image,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NotFoundComponent";
</style>
