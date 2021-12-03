<template>
	<div class="author-container">
		<div class="author-image">
			<router-link :to="`/author-details/${author.id}`">
				<FallbackImage
					:source="author.imageUrl"
					:title="author.name"
					:defaultImage="defaultImage"
				/>
			</router-link>
		</div>
		<div class="author-name">
			<router-link :to="`/author-details/${author.id}`">
				<span>
					{{ author.name }}
				</span>
			</router-link>
		</div>
	</div>
</template>

<script>
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import defaultImage from "@/assets/images/rasters/avatar-placeholder.png";

	export default {
		props: {
			author: Object,
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
	@import "./RecentAuthor.scss";
</style>
