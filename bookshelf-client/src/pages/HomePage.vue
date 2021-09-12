<template>
	<div class="sidebar">
		<CurrentlyReadingList />
	</div>
</template>

<script>
	import { onMounted } from "vue";
	import CurrentlyReadingList from "@/components/Home/ReadingProgress/CurrentlyReadingList.vue";
	export default {
		components: {
			CurrentlyReadingList,
		},
		setup: () => {
			onMounted(() => {
				document.title = "BookShelf - Home Page";
			});
		},
	};
</script>

<style lang="scss" scoped>
	@import "./HomePage";
</style>
