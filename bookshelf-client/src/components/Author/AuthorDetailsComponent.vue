<template>
	<div class="side-container">
		<PopupImage v-if="popupShown" :image="authorImage" @dismiss="toggleAvatarPopup" />
		<div class="avatar-container" @click="toggleAvatarPopup">
			<FallbackImage :source="authorImage" :defaultImage="defaultImage" :title="authorName" />
		</div>
		<BasicAuthorInfo
			:placeOfBirth="author.placeOfBirth"
			:dateOfBirth="author.dateOfBirth"
			:dateOfDeath="author.dateOfDeath"
		/>
	</div>
	<div class="main-container">
		<div class="name-large">{{ authorName }}</div>
		<div class="genres">Genres: {{ genres }}</div>
		<div class="biography">{{ bio }}</div>
		<div class="popular-books-header">Popular books</div>
		<BasicBookList :items="books" />
	</div>
</template>

<script>
	import { ref, computed, onMounted } from "vue";
	import defaultImage from "@/assets/images/rasters/avatar-placeholder.png";
	import FallbackImage from "@/components/Ui/Imaging/FallbackImage.vue";
	import BasicAuthorInfo from "@/components/Author/BasicAuthorInfo.vue";
	import BasicBookList from "@/components/Books/BasicBookList.vue";
	import PopupImage from "../../components/Ui/Imaging/PopupImage.vue";

	export default {
		props: {
			author: Object,
		},
		components: {
			FallbackImage,
			BasicAuthorInfo,
			BasicBookList,
			PopupImage,
		},
		setup: (props) => {
			const popupShown = ref(false);
			const author = computed(() => props.author);
			const authorName = `${author.value.name} ${author.value.surname}`;
			const authorImage = author.value.imageUrl;
			const bio = author.value.bio;
			const books = author.value.bookIssues;
			const genres = author.value.genres?.join(", ");

			onMounted(() => {
				document.title = `${authorName} - Author page`;
			});

			const toggleAvatarPopup = () => {
				popupShown.value = !popupShown.value;
			};

			return {
				authorName,
				authorImage,
				defaultImage,
				bio,
				books,
				genres,
				popupShown,
				toggleAvatarPopup,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./AuthorDetailsComponent";
</style>
