<template>
  <div class="home">
    <HotWord />
  </div>
</template>

<script>
// @ is an alias to /src
import HotWord from "@/components/HotWord.vue";

export default {
  name: "HomeView",
  components: {
    HotWord,
  },
};
</script>
