<template>
	<div class="item-container">
		<div class="book-cover-container">
			<FallbackImage :defaultImage="defaultImage" :source="image" :title="title" />
		</div>
		<div class="book-title">{{ title }}</div>
	</div>
</template>

<script>
	import FallbackImage from "@/components/Ui/FallbackImage.vue";
	import defaultImage from "@/assets/images/rasters/book-placeholder.jpg";

	export default {
		props: {
			title: String,
			image: String,
		},
		components: {
			FallbackImage,
		},
		setup: () => {
			return {
				defaultImage,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./BasicBookListItem.scss";
</style>
