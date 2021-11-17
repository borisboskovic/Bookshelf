<template>
	<div class="left-container">
		<div class="avatar-container">
			<FallbackImage :source="authorImage" :defaultImage="defaultImage" :title="authorName" />
		</div>
		<BasicAuthorInfo
			:placeOfBirth="author.placeOfBirth"
			:dateOfBirth="author.dateOfBirth"
			:dateOfDeath="author.dateOfDeath"
		/>
	</div>
	<div class="right-container">
		<div class="name-large">{{ authorName }}</div>
		<div class="genres">Genres: {{ genres }}</div>
		<div class="biography">{{ bio }}</div>
		<div class="popular-books-header">Popular books</div>
		<BasicBookList :items="books" />
	</div>
</template>

<script>
	import { computed, onMounted } from "vue";
	import defaultImage from "@/assets/images/rasters/avatar-placeholder.png";
	import FallbackImage from "@/components/Ui/FallbackImage.vue";
	import BasicAuthorInfo from "@/components/Author/BasicAuthorInfo.vue";
	import BasicBookList from "@/components/Books/BasicBookList.vue";

	export default {
		props: {
			author: Object,
		},
		components: {
			FallbackImage,
			BasicAuthorInfo,
			BasicBookList,
		},
		setup: (props) => {
			const author = computed(() => props.author);
			const authorName = `${author.value.name} ${author.value.surname}`;
			const authorImage = author.value.imageUrl;
			const bio = author.value.bio;
			const books = author.value.bookIssues;
			const genres = author.value.genres?.join(", ");

			onMounted(() => {
				document.title = `${authorName} - Author page`;
			});

			return {
				authorName,
				authorImage,
				defaultImage,
				bio,
				books,
				genres,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./AuthorDetailsComponent";
</style>
