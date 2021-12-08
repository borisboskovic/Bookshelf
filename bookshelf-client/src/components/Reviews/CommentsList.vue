<template>
	<div v-for="c in comments" :key="c.commentId" class="comment-item__container">
		<div class="comment-item__image-container">
			<FallbackImage :source="c.imageUrl" />
		</div>
		<div class="comment-item__content">
			{{ c.content }}
		</div>
	</div>
</template>

<script>
	import FallbackImage from "../../components/Ui/Imaging/FallbackImage.vue";
	export default {
		props: {
			comments: Array,
		},
		components: {
			FallbackImage,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./CommentsList";
</style>
